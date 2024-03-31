/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface AuthedUser {
  /** @format uuid */
  userId?: string;
}

export interface IdeaIM {
  /**
   * @minLength 2
   * @maxLength 320
   */
  topic: string;
  /**
   * @minLength 2
   * @maxLength 2147483647
   */
  advertPlatforms: string;
  /**
   * @minLength 2
   * @maxLength 320
   */
  targetAudience: string;
  /** @format int32 */
  budget: number;
  /** @minLength 1 */
  tags: string;
  /** @format int32 */
  numberOfCampaigns: number;
}

export interface IdeaVM {
  /** @format uuid */
  id?: string;
  /** @format uuid */
  creatorId?: string;
  topic?: string | null;
  advertPlatforms?: string | null;
  targetAudience?: string | null;
  /** @format int32 */
  budget?: number;
  tags?: string | null;
  /** @format int32 */
  numberOfCampaigns?: number;
}

export interface ProductIM {
  /**
   * @minLength 2
   * @maxLength 320
   */
  name: string;
  /**
   * @minLength 2
   * @maxLength 2147483647
   */
  description: string;
  /**
   * @minLength 2
   * @maxLength 320
   */
  category: string;
  /**
   * @minLength 2
   * @maxLength 2048
   */
  image: string;
}

export interface UserIM {
  /**
   * @minLength 2
   * @maxLength 100
   */
  firstName: string;
  /**
   * @minLength 2
   * @maxLength 100
   */
  lastName: string;
  /**
   * @minLength 1
   * @maxLength 320
   */
  email: string;
  /**
   * @format tel
   * @minLength 1
   */
  phoneNumber: string;
  /** @minLength 1 */
  userName: string;
  /**
   * @minLength 8
   * @maxLength 42
   */
  password: string;
}

export interface UserLoginIM {
  /** @minLength 1 */
  email: string;
  /** @minLength 1 */
  password: string;
}

export type QueryParamsType = Record<string | number, any>;
export type ResponseFormat = keyof Omit<Body, "body" | "bodyUsed">;

export interface FullRequestParams extends Omit<RequestInit, "body"> {
  /** set parameter to `true` for call `securityWorker` for this request */
  secure?: boolean;
  /** request path */
  path: string;
  /** content type of request body */
  type?: ContentType;
  /** query params */
  query?: QueryParamsType;
  /** format of response (i.e. response.json() -> format: "json") */
  format?: ResponseFormat;
  /** request body */
  body?: unknown;
  /** base url */
  baseUrl?: string;
  /** request cancellation token */
  cancelToken?: CancelToken;
}

export type RequestParams = Omit<FullRequestParams, "body" | "method" | "query" | "path">;

export interface ApiConfig<SecurityDataType = unknown> {
  baseUrl?: string;
  baseApiParams?: Omit<RequestParams, "baseUrl" | "cancelToken" | "signal">;
  securityWorker?: (securityData: SecurityDataType | null) => Promise<RequestParams | void> | RequestParams | void;
  customFetch?: typeof fetch;
}

export interface HttpResponse<D extends unknown, E extends unknown = unknown> extends Response {
  data: D;
  error: E;
}

type CancelToken = Symbol | string | number;

export enum ContentType {
  Json = "application/json",
  FormData = "multipart/form-data",
  UrlEncoded = "application/x-www-form-urlencoded",
  Text = "text/plain",
}

export class HttpClient<SecurityDataType = unknown> {
  public baseUrl: string = "";
  private securityData: SecurityDataType | null = null;
  private securityWorker?: ApiConfig<SecurityDataType>["securityWorker"];
  private abortControllers = new Map<CancelToken, AbortController>();
  private customFetch = (...fetchParams: Parameters<typeof fetch>) => fetch(...fetchParams);

  private baseApiParams: RequestParams = {
    credentials: "same-origin",
    headers: {},
    redirect: "follow",
    referrerPolicy: "no-referrer",
  };

  constructor(apiConfig: ApiConfig<SecurityDataType> = {}) {
    Object.assign(this, apiConfig);
  }

  public setSecurityData = (data: SecurityDataType | null) => {
    this.securityData = data;
  };

  protected encodeQueryParam(key: string, value: any) {
    const encodedKey = encodeURIComponent(key);
    return `${encodedKey}=${encodeURIComponent(typeof value === "number" ? value : `${value}`)}`;
  }

  protected addQueryParam(query: QueryParamsType, key: string) {
    return this.encodeQueryParam(key, query[key]);
  }

  protected addArrayQueryParam(query: QueryParamsType, key: string) {
    const value = query[key];
    return value.map((v: any) => this.encodeQueryParam(key, v)).join("&");
  }

  protected toQueryString(rawQuery?: QueryParamsType): string {
    const query = rawQuery || {};
    const keys = Object.keys(query).filter((key) => "undefined" !== typeof query[key]);
    return keys
      .map((key) => (Array.isArray(query[key]) ? this.addArrayQueryParam(query, key) : this.addQueryParam(query, key)))
      .join("&");
  }

  protected addQueryParams(rawQuery?: QueryParamsType): string {
    const queryString = this.toQueryString(rawQuery);
    return queryString ? `?${queryString}` : "";
  }

  private contentFormatters: Record<ContentType, (input: any) => any> = {
    [ContentType.Json]: (input: any) =>
      input !== null && (typeof input === "object" || typeof input === "string") ? JSON.stringify(input) : input,
    [ContentType.Text]: (input: any) => (input !== null && typeof input !== "string" ? JSON.stringify(input) : input),
    [ContentType.FormData]: (input: any) =>
      Object.keys(input || {}).reduce((formData, key) => {
        const property = input[key];
        formData.append(
          key,
          property instanceof Blob
            ? property
            : typeof property === "object" && property !== null
            ? JSON.stringify(property)
            : `${property}`,
        );
        return formData;
      }, new FormData()),
    [ContentType.UrlEncoded]: (input: any) => this.toQueryString(input),
  };

  protected mergeRequestParams(params1: RequestParams, params2?: RequestParams): RequestParams {
    return {
      ...this.baseApiParams,
      ...params1,
      ...(params2 || {}),
      headers: {
        ...(this.baseApiParams.headers || {}),
        ...(params1.headers || {}),
        ...((params2 && params2.headers) || {}),
      },
    };
  }

  protected createAbortSignal = (cancelToken: CancelToken): AbortSignal | undefined => {
    if (this.abortControllers.has(cancelToken)) {
      const abortController = this.abortControllers.get(cancelToken);
      if (abortController) {
        return abortController.signal;
      }
      return void 0;
    }

    const abortController = new AbortController();
    this.abortControllers.set(cancelToken, abortController);
    return abortController.signal;
  };

  public abortRequest = (cancelToken: CancelToken) => {
    const abortController = this.abortControllers.get(cancelToken);

    if (abortController) {
      abortController.abort();
      this.abortControllers.delete(cancelToken);
    }
  };

  public request = async <T = any, E = any>({
    body,
    secure,
    path,
    type,
    query,
    format,
    baseUrl,
    cancelToken,
    ...params
  }: FullRequestParams): Promise<HttpResponse<T, E>> => {
    const secureParams =
      ((typeof secure === "boolean" ? secure : this.baseApiParams.secure) &&
        this.securityWorker &&
        (await this.securityWorker(this.securityData))) ||
      {};
    const requestParams = this.mergeRequestParams(params, secureParams);
    const queryString = query && this.toQueryString(query);
    const payloadFormatter = this.contentFormatters[type || ContentType.Json];
    const responseFormat = format || requestParams.format;

    return this.customFetch(`${baseUrl || this.baseUrl || ""}${path}${queryString ? `?${queryString}` : ""}`, {
      ...requestParams,
      headers: {
        ...(requestParams.headers || {}),
        ...(type && type !== ContentType.FormData ? { "Content-Type": type } : {}),
      },
      signal: (cancelToken ? this.createAbortSignal(cancelToken) : requestParams.signal) || null,
      body: typeof body === "undefined" || body === null ? null : payloadFormatter(body),
    }).then(async (response) => {
      const r = response as HttpResponse<T, E>;
      r.data = null as unknown as T;
      r.error = null as unknown as E;

      const data = !responseFormat
        ? r
        : await response[responseFormat]()
            .then((data) => {
              if (r.ok) {
                r.data = data;
              } else {
                r.error = data;
              }
              return r;
            })
            .catch((e) => {
              r.error = e;
              return r;
            });

      if (cancelToken) {
        this.abortControllers.delete(cancelToken);
      }

      if (!response.ok) throw data;
      return data;
    });
  };
}

/**
 * @title IdeaInvestigator.WebHost
 * @version 1.0
 */
export class Api<SecurityDataType extends unknown> extends HttpClient<SecurityDataType> {
  api = {
    /**
     * No description
     *
     * @tags Auth
     * @name AuthSignupCreate
     * @request POST:/api/auth/signup
     * @secure
     */
    authSignupCreate: (data: UserIM, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/auth/signup`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Auth
     * @name AuthLoginCreate
     * @request POST:/api/auth/login
     * @secure
     */
    authLoginCreate: (data: UserLoginIM, params: RequestParams = {}) =>
      this.request<string, any>({
        path: `/api/auth/login`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Idea
     * @name IdeasHistoryList
     * @request GET:/api/ideas/history
     * @secure
     */
    ideasHistoryList: (params: RequestParams = {}) =>
      this.request<IdeaVM, any>({
        path: `/api/ideas/history`,
        method: "GET",
        secure: true,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Idea
     * @name IdeasNewIdeaCreate
     * @request POST:/api/ideas/newIdea
     * @secure
     */
    ideasNewIdeaCreate: (data: IdeaIM, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/ideas/newIdea`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags ML
     * @name MlGenPostsList
     * @request GET:/api/ml/gen-posts
     * @secure
     */
    mlGenPostsList: (
      data: AuthedUser,
      query?: {
        /** @format uuid */
        ideaId?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<string[], any>({
        path: `/api/ml/gen-posts`,
        method: "GET",
        query: query,
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags ML
     * @name MlGenAdviceList
     * @request GET:/api/ml/gen-advice
     * @secure
     */
    mlGenAdviceList: (
      data: AuthedUser,
      query?: {
        /** @format uuid */
        ideaId?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<string, any>({
        path: `/api/ml/gen-advice`,
        method: "GET",
        query: query,
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags ML
     * @name MlCatIdeaList
     * @request GET:/api/ml/cat-idea
     * @secure
     */
    mlCatIdeaList: (
      data: AuthedUser,
      query?: {
        /** @format uuid */
        ideaId?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<string, any>({
        path: `/api/ml/cat-idea`,
        method: "GET",
        query: query,
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags ML
     * @name MlFindCompetitorsList
     * @request GET:/api/ml/find-competitors
     * @secure
     */
    mlFindCompetitorsList: (
      data: AuthedUser,
      query?: {
        /** @format uuid */
        ideaId?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<string, any>({
        path: `/api/ml/find-competitors`,
        method: "GET",
        query: query,
        body: data,
        secure: true,
        type: ContentType.Json,
        format: "json",
        ...params,
      }),

    /**
     * No description
     *
     * @tags Product
     * @name ProductCreateCreate
     * @request POST:/api/product/create
     * @secure
     */
    productCreateCreate: (data: ProductIM, params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/product/create`,
        method: "POST",
        body: data,
        secure: true,
        type: ContentType.Json,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Product
     * @name ProductGetList
     * @request GET:/api/product/get
     * @secure
     */
    productGetList: (
      query?: {
        /** @format uuid */
        id?: string;
      },
      params: RequestParams = {},
    ) =>
      this.request<void, any>({
        path: `/api/product/get`,
        method: "GET",
        query: query,
        secure: true,
        ...params,
      }),

    /**
     * No description
     *
     * @tags Product
     * @name ProductGetAllList
     * @request GET:/api/product/get-all
     * @secure
     */
    productGetAllList: (params: RequestParams = {}) =>
      this.request<void, any>({
        path: `/api/product/get-all`,
        method: "GET",
        secure: true,
        ...params,
      }),
  };
}
