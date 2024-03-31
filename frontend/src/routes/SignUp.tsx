import { useState, useEffect } from "react"
import { useNavigate } from "react-router-dom"

import { Input } from "@/components/ui/input"
import { Button } from "@/components/ui/button"

import { Api } from "@/api/Api"

import eclipseOne from "/public/Eclipse.png"
import eclipseTwo from "/public/Eclipse1.png"

import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

export default function SignUp() {
    const [firstName, setFirstName] = useState<string>("")
    const [lastName, setLastName] = useState<string>("")
    const [username, setUsername] = useState<string>("")
    const [email, setEmail] = useState<string>("")
    const [phoneNumber, setPhoneNumber] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [confirmPassword, setConfirmPassword] = useState<string>("")

    let navigate = useNavigate()

    useEffect(() => {
        if(localStorage.getItem('token') !== null)
            navigate("/idea-analysis")
    }, [])

    function signUp(e: any) {
        e.preventDefault()

        if (password !== confirmPassword) {
            toast.error("Passwords do not match")
            return
        }

        const client = new Api({
            baseUrl: import.meta.env.VITE_BACKEND_URL,
        });

        client.api.authSignupCreate({
            firstName: firstName,
            lastName: lastName,
            userName: username,
            email: email,
            phoneNumber: phoneNumber,
            password: password
        }).then((response) => {
            if (response.status === 200) {
                navigate("/signin")
            }
        }).catch((error) => {
            toast.error(Object.values(error.errors).join("\n"))
        })

    }

    return(
        <div className="min-w-screen min-h-screen flex">
            <div className="w-2/5 flex text-white flex-col items-center gap-6 justify-center min-h-screen bg-[#09090b] p-16">
                <div className="flex flex-col gap-4 w-full">
                    <h1 className="text-3xl font-bold">Sign Up</h1>
                    <p className="text-[#777]">Welcome to GroWith! Inter your personal details.</p>
                </div>

                <div className="w-full">
                    <form onSubmit={signUp} className="flex flex-col gap-2">
                        <div className="flex w-full gap-2">
                            <div className="w-full gap-2 flex flex-col">
                                <Input onChange={(e) => {setFirstName(e.target.value)}} placeholder="First Name" />
                                <Input onChange={(e) => {setLastName(e.target.value)}} placeholder="Last Name" />
                                <Input onChange={(e) => {setUsername(e.target.value)}} placeholder="Username" />
                            </div>

                            <div className="w-full gap-2 flex flex-col">
                                <Input onChange={(e) => {setEmail(e.target.value)}} type="email" placeholder="Email" />
                                <Input onChange={(e) => {setPhoneNumber(e.target.value)}} placeholder="Phone number" />
                                <Input onChange={(e) => {setPassword(e.target.value)}} type="password" placeholder="Password" />
                            </div>
                        </div>

                        <Input onChange={(e) => {setConfirmPassword(e.target.value)}} type="password" placeholder="Confirm Password" />

                        <Button type="submit" className="mt-4">Sign up</Button>
                    </form>
                </div>

                <div className="flex w-full relative mt-4">
                    <p className="text-[#777]">Already have an account?</p>
                    <Button onClick={() => {navigate("/signin")}} className="absolute right-0">Sign in</Button>
                </div>
            </div>

            <div className="w-3/5 relative min-h-full flex justify-center text-white items-center max-lg:w-full py-8">
                <img src={eclipseOne} className="absolute top-0 select-none" draggable={false} alt="" />
                <img src={eclipseTwo} className="absolute bottom-0 right-0 select-none" draggable={false} alt="" />

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
                <ToastContainer />
        </div>
    )
}
