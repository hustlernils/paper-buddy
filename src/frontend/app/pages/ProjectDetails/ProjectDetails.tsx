import { useParams } from "react-router-dom"
import { Button } from "../../components/ui/Button";
import { Toolbar } from "../../components/layout/Toolbar";
import { useProjects } from "../../hooks/useProjects";
import { Card, CardHeader, CardTitle, CardDescription, CardContent, CardAction } from "../../components/ui/Card";
import { Separator } from "../../components/ui/separator";
import { usePapers } from "../../hooks/usePapers";
import { type GetPapersResponse } from "../../types/api";
import { Grid } from '../../components/layout/Grid'
import { GridItem } from '../../components/layout/GridItem'
import { ProjectChats } from "./ProjectChats";
import { useChats } from "../../hooks/useChats";
import { Item, ItemGroup } from "../../components/ui/item";
import UploadPaperDialog from "../../components/UploadPaperDialog"

export const ProjectDetails = () => 
{
  const { projectId } = useParams<{projectId: string}>(); 
  const { getCurrentProject } = useProjects();
  const { papers } = usePapers();
  const { createChat } = useChats(projectId, 'Project');

  const currentProject = getCurrentProject(projectId)
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
            <CardHeader>
              <CardTitle>Project description</CardTitle>
            </CardHeader>   
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
            <CardHeader>
              <CardTitle>Related Papers</CardTitle>
              <CardAction>
                <UploadPaperDialog projectId={projectId}>
                  <Button>Upload Paper</Button>
                </UploadPaperDialog>
              </CardAction>
            </CardHeader>
            <Separator />
            <CardContent>
              <ItemGroup>
                <div className="space-y-2">
                  {papers.map((paper: GetPapersResponse, index: number) => (
                    <Item variant="outline">
                      <div key={index}>
                        <h3>{paper.title}</h3>
                        <Separator/>
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