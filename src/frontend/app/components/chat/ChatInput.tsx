import { Input } from "../ui/Input"
import { useState } from "react"
import { Button } from "../ui/Button"

export interface ChatInputProps {
    onSubmit: (content: string) => void
    disabled?: boolean
}

export const ChatInput = ({ onSubmit, disabled = false }: ChatInputProps) => 
{
  const [message, setMessage] = useState<string>("")

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => 
  {
    setMessage(e.target.value)
  }

  const handleSubmit = (e : React.FormEvent) =>
  {
    e.preventDefault()
    if (disabled) return
    onSubmit(message)
    setMessage("")
  }

  return(
    <div className="absolute bottom-0 left-0 right-0 bg-background border-t pt-4">
      <form className="flex flex-row gap-2 max-w-4xl mx-auto" onSubmit={handleSubmit}>
        <Input className="flex-1" value={message} onChange={handleChange} disabled={disabled}></Input>
        <Button type="submit" disabled={disabled}>Send</Button>
      </form>
    </div>
  )
}