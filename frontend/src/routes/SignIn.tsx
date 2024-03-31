import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"

import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"

import { Api } from "@/api/Api"
import { useToast } from "@/components/ui/use-toast"

export default function SignIn() {
    const { toast } = useToast()

    const [email, setEmail] = useState<string>("")
    const [password, setPassword] = useState<string>("")

    let navigate = useNavigate()

    useEffect(() => {
        if(localStorage.getItem('token') !== null)
            navigate("/idea-analysis")
    }, [])

    function signIn(e: any) {
        e.preventDefault()

        const client = new Api({
            baseUrl: import.meta.env.VITE_BACKEND_URL,
        });

        client.api.authLoginCreate({
            email: email,
            password: password
        }).then((response) => {
            if (response.status === 200) {
                localStorage.setItem("token", response.data.token)
                localStorage.setItem("userId", response.data.userId)
                navigate("/idea-analysis")
            }
        }).catch((error) => {
            toast({
                title: "Error",
                description: error,
            })
        })

    }

    return(
        <div className="min-w-screen min-h-screen flex">
            <div className="w-2/5 flex text-white flex-col items-center gap-6 justify-center min-h-screen bg-[#09090b] p-16">
                <div className="flex flex-col gap-4 w-full">
                    <h1 className="text-3xl font-bold">Sign In</h1>
                    <p className="text-[#777]">Welcome to GroWith! Inter your personal details.</p>
                </div>

                <div className="w-full">
                    <form onSubmit={signIn} className="flex flex-col gap-2">
                        <Input type="email" onChange={(e) => {setEmail(e.target.value)}} placeholder="Email" />
                        <Input onChange={(e) => {setPassword(e.target.value)}} type="password" placeholder="Password" />

                        <Button type="submit" className="mt-4">Sign In</Button>
                    </form>
                </div>

                <div className="flex w-full relative mt-4">
                    <p className="text-[#777]">Don't have an account?</p>
                    <Button onClick={() => {navigate("/signup")}} className="absolute right-0">Sign up</Button>
                </div>
            </div>

            <div className="w-3/5 relative min-h-full flex justify-center text-white items-center max-lg:w-full py-8">
                <img src="/public/Eclipse.png" className="absolute top-0 select-none" draggable={false} alt="" />
                <img src="/public/Eclipse1.png" className="absolute bottom-0 right-0 select-none" draggable={false} alt="" />

                    <div className="flex items-center justify-center flex-col z-10 w-[450px] gap-4">
                        <div className="flex items-left justify-center flex-col w-full">
                            <h1 className="text-[80px] font-bold whitespace-nowrap">Welcome to</h1>
                            <h1 className="text-[80px] font-bold">GroWith</h1>
                        </div>

                        <div className="flex flex-col justify-center items-left w-full gap-8 text-lg">
                            <p>Sign up or Log in to access your account and set up your account.</p>
                            <p>Our project consists of evaluation of your start up by comparing it to another already successful businesses. It provides you with a success rate procentage determined by the latest trends</p>
                        </div>
                    </div>
                </div>
        </div>
    )
}