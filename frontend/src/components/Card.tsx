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
                    <div className="w-fit duration-150 cursor-pointer h-[450px] overflow-hidden bg-[#4a4e69] p-4 rounded-xl hover:bg-[#373a4e] flex flex-col items-center gap-4">
                        <img src={image} className="rounded-xl drop-shadow-sm" alt="" />

                        <div className="text-center">
                            <h1 className="font-bold">{title}</h1>
                            <p className="max-w-[90ch]">{description} </p>
                        </div>
                    </div> 
                </SheetTrigger>
                <SheetContent className="flex flex-col dark text-white">
                    <img src={image} className="rounded-xl drop-shadow-sm" alt="" />

                    <div className="text-center">
                        <h1 className="font-bold">{title}</h1>
                        <p className="max-w-[90ch]">{description} </p>
                    </div>
                </SheetContent>
            </Sheet>
        </>
    )
}