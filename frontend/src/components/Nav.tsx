import { Button } from "./ui/button"

export default function Nav() {
    return (
        <>
            <div className="w-full h-11 fixed top-0 left-0 bg-[#ffffff30] backdrop-blur-2xl flex">
                <div className="h-full flex justify-center items-center ml-2">
                    <Button className="rounded-full" size="icon"></Button>  
                </div>

                <div className="absolute right-2 h-full gap-3 w-fit flex justify-center items-center">
                    <Button>Sign in</Button>
                    <Button>Sign up</Button>
                </div>
            </div>
        </>
    )
}