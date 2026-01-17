import { ChatList } from "../../components/chat/ChatList";
import { useChats } from "../../hooks/useChats";
import { Card, CardContent, CardTitle } from "../../components/ui/Card";
import { useNavigate } from "react-router-dom";

export interface ProjectChatsProps {
  projectId: string | undefined
}

export const ProjectChats = ( { projectId }: ProjectChatsProps) => {
  const { chats } = useChats(projectId, 'Project');
  const navigate = useNavigate();

  const openChat = (index: number) => 
  {
    const chatId = chats[index].id;
    navigate(`/chats/${chatId}`)
  }

    return (
      <Card>
        <CardTitle>Chats on this project</CardTitle>
        <CardContent>
            <ChatList chats={chats} openChat={openChat}/>
        </CardContent>
      </Card>
    )
}