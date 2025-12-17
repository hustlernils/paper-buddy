import { Input } from "../ui/Input"
import { useState } from "react"
import { Button } from "../ui/Button"

export interface ChatInputProps {
    onSubmit: (content: string) => void
}

export const ChatInput = ({ onSubmit }: ChatInputProps) => {
    const [message, setMessage] = useState<string>("")

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setMessage(e.target.value)
    }

    return(
        <form onSubmit={(e : React.FormEvent) => {
            e.preventDefault()
            onSubmit(message)
        }}>
            <Input value={message} onChange={handleChange}></Input>
            <Button role="submit">Send</Button>
        </form>
    )
}