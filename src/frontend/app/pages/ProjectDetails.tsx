import { useEffect, useState } from "react";
import { useParams } from "react-router-dom"
import { useFetch } from "../hooks/useFetch";
import { Button } from "../components/ui/Button";
import { Toolbar } from "../components/layout/Toolbar";
import { useProjects } from "../hooks/useProjects";

interface ChatResponse{
    id: string,
    createdAt: string
};

interface CreateChatRequest{
    parentType: string,
    parentId: string
}

export const ProjectDetails = () => {
    const { api } = useFetch();
    const { id } = useParams<{id: string}>(); 
    const { projects } = useProjects();
    const [chats, setChats] = useState<ChatResponse[]>([]);
    
    const currentProject = projects.find(p => p.id === id)

    const fetchChats = async () => {
        const chatsResponse = await api.get<ChatResponse[]>(`/chats/${id}?parentType=project`)
        if (chatsResponse){
            setChats(chatsResponse)
        }
    }

    const createChat = async () => {
        await api.post("/chats", {
            parentType: "project",
            parentId: id
        })
        fetchChats()
    }

    useEffect(() => {
        fetchChats()
    }, [])    


    return(
        <>
        <Toolbar title={currentProject?.title}>
            <Button onClick={() => createChat()}>New Chat</Button>
        </Toolbar>
        <h1>Your chats</h1>
        {chats.map((chat) => {
            <div>{chat.createdAt}</div>
        })}
        </>
    )
}