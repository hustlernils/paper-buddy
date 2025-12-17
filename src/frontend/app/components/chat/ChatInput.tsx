import { Input } from "../ui/Input"
import { useState } from "react"
import { Button } from "../ui/Button"

export interface ChatInputProps {
    onSubmit: (content: string) => void
}

export const ChatInput = ({ onSubmit }: ChatInputProps) => 
{
  const [message, setMessage] = useState<string>("")

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => 
  {
    setMessage(e.target.value)
  }

  const handleSubmit = (e : React.FormEvent) =>
  {
    e.preventDefault()
    onSubmit(message)
    setMessage("")
  }

  return(
    <div className="absolute bottom-0 left-0 right-0 bg-background border-t pt-4">
      <form className="flex flex-row gap-2 max-w-4xl mx-auto" onSubmit={handleSubmit}>
        <Input className="flex-1" value={message} onChange={handleChange}></Input>
        <Button type="submit">Send</Button>
      </form>
    </div>
  )
}