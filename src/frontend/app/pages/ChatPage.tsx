import { useParams, useSearchParams } from "react-router-dom";
import { useChats } from "../hooks/useChats";
import { useEffect } from "react";
import { Chat } from "../components/chat/Chat";
import { type ParentType } from "../types/api";

export const ChatPage = () =>{
    const { chatId } = useParams<{chatId: string}>(); 
    const [searchParams] = useSearchParams();

    const parentType = searchParams.get("parentType") as ParentType
    const parentId = searchParams.get("parentId")

    const { activeChat, setActiveChat, chatMessages, sendChatMessage} = useChats(parentId, parentType);
    
    useEffect(() =>{
      if(chatId){
        setActiveChat(chatId)
      }  
    })
    
    return (
        <>
          {activeChat && <Chat id={activeChat} messages={chatMessages} onSubmit={sendChatMessage}></Chat>}          
        </>
    )
}