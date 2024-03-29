import { useNavigate } from "react-router-dom"

import { Button } from "./ui/button"

export default function Nav() {
    let navigate = useNavigate()

    return (
        <>
            <div className="w-full h-11 fixed top-0 left-0 bg-[#ffffff30] backdrop-blur-2xl flex">
                <div className="h-full flex justify-center items-center ml-2">
                    <Button className="rounded-full" size="icon"></Button>  
                </div>

                <div className="absolute right-2 h-full gap-3 w-fit flex justify-center items-center">
                    <Button onClick={() => {navigate("/signin")}}>Sign in</Button>
                    <Button onClick={() => {navigate("/signup")}}>Sign up</Button>
                </div>
            </div>
        </>
    )
}