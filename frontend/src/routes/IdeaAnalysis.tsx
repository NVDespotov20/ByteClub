import { useEffect, useState } from "react"
import { ArrowRightFromLine, SquareMenu } from 'lucide-react'
import { Api } from "@/api/Api"

import { Textarea } from "@/components/ui/textarea"
import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import Card from "@/components/Card"
import HistoryEntry from "@/components/HistoryEntry"
import { useToast } from "@/components/ui/use-toast"

import {
  Sheet,
  SheetContent,
  SheetTrigger,
} from "@/components/ui/sheet"

export default function IdeaAnalysis() {
    const { toast } = useToast()
    const [idea, setIdea] = useState<string>("")

    const [advertPlatforms, setAdvertPlatforms] = useState<string>("")
    const [targetAudience, setTargetAudience] = useState<string>("")
    const [budget, setBudget] = useState<number>(0)
    const [tags, setTags] = useState<string>("")
    const [numberOfCampaigns, setNumberOfCampaigns] = useState<number>(0)

    const [isFormSubmitted, setIsFormSubmitted] = useState<boolean>(false)

    const [gptResponse, setGptResponse] = useState<string>("")

    const [history, setHistory] = useState<string[]>([])
    const [newIdeaComplete, setNewIdeaComplete] = useState()

    const [isIdeaSubmitted, setIsIdeaSubmitted] = useState<boolean>(false)

    function submitForm(e: any) {
        e.preventDefault()
        setIsFormSubmitted(true)

        const client = new Api(
            {
                baseUrl: import.meta.env.VITE_BACKEND_URL as string,
            }
        )

        client.api.ideasNewIdeaCreate({
            topic: idea,
            advertPlatforms: advertPlatforms,
            targetAudience: targetAudience,
            budget: budget,
            tags: tags,
            numberOfCampaigns: numberOfCampaigns,
        }, {
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')!
            }
        }).then((response) => {
            setNewIdeaComplete(response.data)
            setIsFormSubmitted(true)
        }).catch((error) => {
            toast({
                title: "Error",
                description: Object.values(error.errors).join("\n"),
            })
        })

    }
    
    function submitIdea(e: any) {
        e.preventDefault()
        setIsIdeaSubmitted(true) 

        setAdvertPlatforms("Test platformer")
        setBudget(1000)
        setTargetAudience("18-29")
        setNumberOfCampaigns(4)
        setTags("newTag")
    }

    useEffect(() => {
        const client = new Api(
            {
                baseUrl: import.meta.env.VITE_BACKEND_URL as string,
            }
        )

        client.api.ideasHistoryList({
            headers: {
                'Authorization': 'Bearer ' + localStorage.getItem('token')
            }
        }).then((response) => {
            setHistory(response.data.reverse())
        })
    }, [newIdeaComplete])

    document.body.style.overflow = "auto"

    return (
        <>
            <div className="min-w-screen min-h-screen">
                <img src="/public/gradient-green.png" draggable={false} className="fixed select-none left-0 top-0" alt="" />
                <img src="/public/gradient-purple.png" draggable={false} className="fixed select-none right-0 top-0" alt="" />

                <Sheet>
                    <SheetTrigger>
                        <Button size="icon" className="absolute right-5 top-5 flex justify-center items-center">
                            <SquareMenu />
                        </Button>
                    </SheetTrigger>
                    <SheetContent className="flex flex-col text-white dark">
                        <div className="mt-5">
                            {
                                history.map((item, index) => {
                                    return <HistoryEntry key={index} title={item} clickHandler={() => {setIdea(item)}} />
                                })
                            }
                        </div>
                    </SheetContent>
                </Sheet>

                <h1 className="text-white text-8xl z-10 text-center relative mt-10 font-bold">Idea Analysis</h1>
                <div className="w-full h-fit flex flex-col justify-center mt-32 text-white items-center gap-4">
                    <form className="flex justify-center items-center gap-8 w-1/2">
                        <Textarea onChange={(e) => {setIdea(e.target.value)}} value={idea} className="resize-none z-10 border-none bg-[#2a3952] p-5 text-lg" placeholder="Describe your idea"/>
                        <Button onClick={submitIdea} type="submit" size="icon" className="w-14 h-14 rounded-full z-10 bg-[#172544] flex justify-center items-center">
                            <ArrowRightFromLine />
                        </Button>
                    </form>

                    {isIdeaSubmitted && <form onSubmit={submitForm} className="flex flex-col mt-4 gap-4 w-1/2 mb-4">
                        <div className="flex flex-col gap-2 z-10">
                            <Label className="text-lg" htmlFor="advrtPlat"> Where do you advertise your product? </Label>
                            <Input onChange={(e: any) => setAdvertPlatforms(e.target.value)} value={advertPlatforms} name="adverPlat" className="p-4 bg-[#ffffff30] backdrop-blur-xl text-lg w-full border-[#555]" />
                        </div>

                        <div className="flex flex-col gap-2 z-10">
                            <Label className="text-lg" htmlFor="audience"> What is your target audience? </Label>
                            <Input onChange={(e: any) => setTargetAudience(e.target.value)} value={targetAudience} name="auience" className="p-4 text-lg w-full bg-[#ffffff30] backdrop-blur-xl border-[#555]" />
                        </div>

                        <div className="flex flex-col gap-2 z-10">
                            <Label className="text-lg" htmlFor="budget"> What is your budget? </Label>
                            <Input onChange={(e: any) => setBudget(e.target.value)} value={budget} type="number" name="budget" className="p-4 text-lg bg-[#ffffff30] backdrop-blur-xl w-full border-[#555]" />
                        </div>

                        <div className="flex flex-col gap-2 z-10">
                            <Label className="text-lg" htmlFor="tags"> What are your tags? </Label>
                            <Input onChange={(e: any) => setTags(e.target.value)} value={tags} name="tags" className="p-4 text-lg w-full bg-[#ffffff30] backdrop-blur-xl border-[#555]" />
                        </div>

                        <div className="flex flex-col gap-2 z-10">
                            <Label className="text-lg" htmlFor="camapign"> How many campaigns there are? </Label>
                            <Input onChange={(e: any) => setNumberOfCampaigns(e.target.value)} type="number" value={numberOfCampaigns} name="campign" className="p-4 text-lg bg-[#ffffff30] backdrop-blur-xl w-full border-[#555]" />
                        </div>

                        <Button type="submit" className="z-10">Submit Form</Button>
                    </form>}
                    
                    {isFormSubmitted && <div className="h-full z-10 grid grid-cols-3 place-items-center mt-10 gap-10">
                    </div>}

                    {isFormSubmitted && <div className="w-full z-10 flex-col gap-4 flex my-10 justify-center items-center">
                        <h1 className="text-2xl font-bold">Improvment Suggestions</h1>
                        <p className="text-lg max-w-[90ch] text-center">{gptResponse}</p>
                    </div>}
                </div>
            </div>
        </>
    )
}