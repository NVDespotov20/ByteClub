import { useNavigate } from "react-router-dom"
import { Button } from "@/components/ui/button"
import oringleCicle from "/public/gradient-orange.png"
import purpleCicle from "/public/gradient-purple-2.png"

export default function Home() {
    const navigate = useNavigate()

    document.body.style.overflow = "hidden"

    return (
        <>
            <div className="min-w-screen max-h-screen flex justify-center">
                <img src={purpleCicle} className="absolute select-none right-0" draggable={false} alt="" />
                <img src={oringleCicle} className="absolute left-0 select-none" draggable={false} alt="" />

                <div className="w-1/2 z-10 text-white gap-8 flex items-center mt-[250px] flex-col">
                    <div className="flex flex-col justify-center items-center gap-4">
                        <h1 className="text-8xl font-bold drop-shadow-sm">GroWith</h1>
                        <p className="text-xl text-[#888]">Our projects aim is to help you get your startup from the scratch to success.</p>
                    </div>

                    <div className="flex gap-5">
                        <Button onClick={() => {navigate("/signin")}} className="p-8 w-full text-2xl">Log In</Button>
                        <Button onClick={() => {navigate("/signup")}} className="p-8 w-full text-2xl">Sign Up</Button>
                    </div>
                </div>
            </div>
        </>
    )
}