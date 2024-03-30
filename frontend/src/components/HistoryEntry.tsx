
export default function HistoryEntry({ title, clickHandler }: any) {
    return (
        <div onClick={clickHandler} className="w-full h-8 text-white justify-center items-center cursor-pointer rounded-xl p-6 flex hover:bg-[#ffffff30]">
            <p className="text-center">{title}</p>
        </div>
    )
}