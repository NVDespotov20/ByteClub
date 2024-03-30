import requests
from bs4 import BeautifulSoup
import concurrent.futures
import psycopg2

link = "https://www.amazon.com/s?k="

conn: psycopg2.extensions.connection = psycopg2.connect("host=byteclubdb.postgres.database.azure.com dbname=postgres user=qp password=QnakaPalqka123 sslmode=require")
curr: psycopg2.extensions.cursor = conn.cursor()

def scrape_product(href, headers):
    try:
        soup = BeautifulSoup(requests.get("https://www.amazon.com" + href, headers=headers).content, features="html.parser")
        return soup
    except Exception as e:
        print(f"Error scraping product: {e}")
        return None

def process_product(href, headers, category):
    product_info = scrape_product(href, headers)
    if product_info:
        title = product_info.select_one('#productTitle')
        description = product_info.select_one('#productDescription')
        image = product_info.select_one('#landingImage')
        # print(title)
        # print(description)
        # print(image)
        if title and description and image:
            print("\033[1;34mScraping product...\033[0m")
            product = {
                'title': title.text.strip(),
                'description': description.text.strip(),
                'image': image['src'],
                'category': category
            }

            curr.execute("""INSERT INTO "Products" ("Name", "Description", "Category", "Image")
                        VALUES 
                            (%s, %s, %s, %s);""", (product['title'], product['description'], product['category'], product['image']))



def process_category(category):
    query = link + category
    headers = {
        'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:124.0) Gecko/20100101 Firefox/124.0',
        'Accept-Language': 'da, en-gb, en',
        'Accept-Encoding': 'gzip, deflate, br',
        'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8',
        'Referer': 'https://www.google.com/'
    }

    req_res = requests.get(query, headers=headers)
    if req_res.status_code != 200:
        print(f"\033[1;31mError: {req_res.status_code}\033[0m")
        return

    soup = BeautifulSoup(req_res.content, features="html.parser")

    products_soup = soup.find('div', id='search')

    hrefs = [a['href'] for a in products_soup.find_all('a', attrs={'class': 'a-link-normal s-no-outline'}) if a['href'][0] == '/']

    with concurrent.futures.ThreadPoolExecutor() as executor:
        executor.map(process_product, hrefs, [headers]*len(hrefs), [category]*len(hrefs), chunksize=1)


product_cats = [
    'Electronics',
    'Clothing & Apparel',
    'Home & Kitchen',
    'Beauty & Personal Care',
    'Health & Wellness',
    'Sports & Outdoors',
    'Automotive & Accessories',
    'Toys & Games',
    'Books & Literature',
    'Tools & Home Improvement',
    'Food & Beverages',
    'Furniture & Decor',
    'Pet Supplies',
    'Office Supplies',
    'Baby & Toddler',
    'Jewelry & Accessories',
    'Electronics Accessories',
    'Fitness & Exercise',
    'Arts & Crafts',
    'Garden & Outdoor Living',
    'Travel & Luggage',
    'Watches',
    'Party & Celebration Supplies',
    'Musical Instruments',
    'Photography & Video',
    'Gaming & Entertainment',
    'Collectibles & Memorabilia',
    'Home Appliances',
    'DIY & Home Renovation',
    'Camping & Hiking',
    'Shoes & Footwear',
    'Stationery & Writing Supplies',
    'Personal Electronics',
    'Fishing & Hunting',
    'Hair Care & Styling',
    'Baking & Cooking Supplies',
    'Board Games & Puzzles',
    'Home Organization',
    'Mobile Phones & Accessories',
    'Cosmetics',
    'Cycling & Biking',
    'Handbags & Purses',
    'Educational Toys & Games',
    'Watches & Wearables',
    'Audio Equipment',
    'Party Supplies',
    'Skin Care',
    'Specialty Foods',
    'Outdoor Recreation',
    'Bedding & Linens',
    'Travel Accessories',
    'Sunglasses & Eyewear',
    'Video Games',
    'Cookware & Bakeware',
    'Home Decor',
    'Car Parts & Accessories',
    'Fitness Equipment',
    'Water Sports Gear',
    'Fine Jewelry',
    'Paintings & Wall Art',
    'Small Appliances',
    'Craft Supplies',
    'Bath & Shower Products',
    'Action Figures & Collectibles',
    'Candles & Home Fragrance',
    'Running & Athletic Gear',
    'Watches & Jewelry Accessories',
    'Golf Equipment',
    'Yoga & Pilates Equipment',
    'Cycling Accessories',
    'Kitchen Appliances',
    'Personal Care Appliances',
    'Dance & Gymnastics Gear',
    'Skincare Tools & Accessories',
    'Photography Accessories',
    'Musical Equipment & Accessories',
    'Electronics Gadgets',
    'Makeup & Cosmetics Accessories',
    'Hair Accessories',
    'Fishing Gear & Accessories',
    'Climbing & Mountaineering Gear',
    'Bags & Backpacks',
    'Martial Arts Gear',
    'Hiking Gear',
    'RV & Camper Accessories',
    'Perfumes & Fragrances',
    'Weightlifting & Strength Training Equipment',
    'Smart Home Devices',
    'Novelty Gifts',
    'Safety & Security Products',
    'Camping Gear',
    'Collectible Figurines',
    'Fitness Apparel',
    'Eco-Friendly Products',
    'Farming & Agriculture Equipment',
    'Kitchen Gadgets',
    'Fitness Trackers & Smartwatches',
    'Science Kits & Educational Toys',
    'Pool & Spa Equipment',
    'Birding Equipment & Accessories'
]

# run all categories at the same time using multithreading and wait until all are done before exiting and closing the connection
with concurrent.futures.ThreadPoolExecutor() as executor:
    tasks = [executor.submit(process_category, product_cat) for product_cat in product_cats]

    for task in concurrent.futures.as_completed(tasks):
        res = task.result()

    conn.commit()
