import {
  Sheet,
  SheetContent,
  SheetTrigger,
} from "@/components/ui/sheet"

export default function Card({ title, description, image}: any) {
    return (
        <>
            <Sheet>
                <SheetTrigger>
                    <div className="w-fit mx-16 z-10 relative duration-150 cursor-pointer h-[450px] overflow-hidden bg-[#ffffff30] backdrop-blur-xl p-4 rounded-xl hover:border hover:border-1 hover:border-[#fff] flex flex-col items-center gap-4">
                        <img src={image} className="rounded-xl h-1/2 object-contain drop-shadow-sm" alt="" />

                        <div className="text-center w-fit">
                            <h1 className="font-bold max-w-[70ch]">{title}</h1>
                            <p className="max-w-[70ch]">{description} </p>
                        </div>
                    </div> 
                </SheetTrigger>
                <SheetContent className="flex flex-col dark text-white bg-[#ffffff30] backdrop-blur-xl">
                    <img src={image} className="rounded-xl mt-12 drop-shadow-sm" alt="" />

                    <div className="text-center">
                        <h1 className="font-bold">{title}</h1>
                        <p className="max-w-[90ch]">{description} </p>
                    </div>
                </SheetContent>
            </Sheet>
        </>
    )
}