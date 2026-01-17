import { useState, useEffect } from "react"
import { useFetch } from "./useFetch"
import { type ParentType } from "../types/api"
import { type ChatResponse, type ChatMessageResponse } from "../types/api"

export interface UseChatsResponse {
  chats: ChatResponse[]
  activeChat: string | null
  chatMessages: ChatMessageResponse[]
  fetchChats: (parentId: string, parentType: ParentType) => void
  createChat: (parentId: string, parentType: ParentType) => void
  setActiveChat: (chatId: string) => void
  sendChatMessage: (content: string) => void
}

export const useChats = (parentId?: string | undefined | null, parentType?: ParentType | null): UseChatsResponse => 
{
  const { api } = useFetch();
  const [chats, setChats] = useState<ChatResponse[]>([]);
  const [activeChat, setActiveChat] = useState<string | null>(null)
  const [chatMessages, setChatMessages] = useState<ChatMessageResponse[]>([])

  useEffect(() => 
  {
    if (parentId && parentType){
      fetchChats(parentId, parentType)
    }
  }, [parentId])

  useEffect(() => 
  {
    if (activeChat)
    {
      fetchChatMessages(activeChat)
    }
  }, [activeChat])

  const fetchChats = async (parentId: string, parentType: ParentType) => 
  {
    const chatsResponse = await api.get<ChatResponse[]>(`/chats/${parentId}?parentType=${parentType}`)
    if (chatsResponse)
    {
      setChats(chatsResponse)
    }
  }

  const createChat = async (parentId: string, parentType: ParentType) => 
  {
    await api.post("/chats", {
      parentType: parentType,
      parentId: parentId
    })
    fetchChats(parentId, parentType)
  }

  const sendChatMessage = async (content: string) =>
  {
    if(activeChat)
    {
      await api.post(`/chats/${activeChat}/messages`, { content: content})
      await fetchChatMessages(activeChat)
    }

    setChatMessages(prev => [
      ...prev,
      {content, createdAt: "", role: "User"}])
  }

  const fetchChatMessages = async (chatId : string) => 
  {
    const response = await api.get<ChatMessageResponse[]>(`/chats/${chatId}/messages`)
    if (response)
    {
      setChatMessages(response)
    }
  }

  return { chats, activeChat, chatMessages, fetchChats, createChat, sendChatMessage, setActiveChat}
}