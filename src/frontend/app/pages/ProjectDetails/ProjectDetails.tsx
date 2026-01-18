import { useParams } from "react-router-dom"
import { Button } from "../../components/ui/Button";
import { Toolbar } from "../../components/layout/Toolbar";
import { useProjects } from "../../hooks/useProjects";
import { Card, CardHeader, CardDescription, CardContent } from "../../components/ui/Card";
import { Input } from "../../components/ui/Input";
import { Label } from "../../components/ui/Label";
import { Separator } from "../../components/ui/Separator";
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
  const [notes, setNotes] = useState<string[]>(["Initial project note", "Research hypothesis"]);
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
              <CardHeader>Notes</CardHeader>
              <ul className="list-disc pl-5">
                {notes.map((note, index) => <li key={index}>{note}</li>)}
              </ul>
              <Separator />
              <div className="space-y-2">
                <Label htmlFor="new-note">Add Note</Label>
                <Input id="new-note" value={newNote} onChange={(e: ChangeEvent<HTMLInputElement>) => setNewNote((e.target as HTMLInputElement).value)} />
                <Button onClick={addNote}>Add</Button>
              </div>
            </Card>
          </GridItem>
          <GridItem className="col-span-3 row-span-2 row-start-1">
            <Card>
              <CardHeader>Related Papers</CardHeader>
              <CardDescription>
              All papers asociated with this project
              </CardDescription>
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