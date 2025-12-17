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
    <form className="flex flex-row gap-2" onSubmit={handleSubmit}>
      <Input className="flex-6" value={message} onChange={handleChange}></Input>
      <Button className="flex-1" role="submit">Send</Button>
    </form>
  )
}