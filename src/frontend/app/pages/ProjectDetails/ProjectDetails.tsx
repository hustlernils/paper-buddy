import { useParams } from "react-router-dom"
import { Button } from "../../components/ui/Button";
import { Toolbar } from "../../components/layout/Toolbar";
import { useProjects } from "../../hooks/useProjects";
import { Card, CardHeader, CardDescription, CardContent, CardTitle } from "../../components/ui/Card";
import { Input } from "../../components/ui/Input";
import { Label } from "../../components/ui/Label";
import { Separator } from "../../components/ui/separator";
import { usePapers } from "../../hooks/usePapers";
import { useState, type ChangeEvent } from "react";
import { type GetPapersResponse } from "../../types/api";
import { Grid } from '../../components/layout/Grid'
import { GridItem } from '../../components/layout/GridItem'
import { ProjectChats } from "./ProjectChats";
import { useChats } from "../../hooks/useChats";
import { Item, ItemGroup } from "../../components/ui/item";

export const ProjectDetails = () => 
{
  const { projectId } = useParams<{projectId: string}>(); 
  const { getCurrentProject } = useProjects();
  const { papers } = usePapers();
  const [newNote, setNewNote] = useState<string>("");
  const { createChat } = useChats(projectId, 'Project');

  const currentProject = getCurrentProject(projectId)

  const addNote = () => 
  {
    if (newNote.trim()) 
    {
      setNotes([...notes, newNote]);
      setNewNote("");
    }
  }

  const handleCreateChat = () => 
  {
    if (projectId) 
    {
      createChat(projectId, 'Project')
    }   
  }

  return(
    <>
      <Toolbar title={currentProject?.title}>
        <Button onClick={() => handleCreateChat()}>New Chat</Button>
      </Toolbar>
        <Grid>
          <GridItem className="row-start-1 col-span-3 row-span-1">
            <Card>
              <CardHeader>Project description</CardHeader>   
                <Separator />
                <CardContent>
                <CardDescription>
                {currentProject?.description}
                </CardDescription>
              </CardContent>
            </Card>
          </GridItem>
          <GridItem className="col-span-3 row-span-2 row-start-1">
            <Card>
              <CardHeader>Related Papers</CardHeader>
              <CardContent>
              <CardDescription>
              All papers asociated with this project
              </CardDescription>
              </CardContent>
              <Separator/>
            <CardContent>
              <ItemGroup>
                <div className="space-y-2">
                  {papers.map((paper: GetPapersResponse, index: number) => (
                    <Item variant="outline">
                      <div key={index}>
                      <p className="text-sm text-gray-600">{paper.authors}</p>
                      </div>
                    </Item>                    
                  ))}
                </div>
              </ItemGroup>              
            </CardContent>
          </Card>
          </GridItem>
          <GridItem className="row-start-2 col-span-3">
            <ProjectChats projectId={projectId}/>
          </GridItem>      
      </Grid>
    </>
  )
}