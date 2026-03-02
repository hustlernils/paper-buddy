import { Badge } from "../components/ui/Badge"
import { Card, CardDescription, CardHeader } from "../components/ui/Card"
import { Separator } from "../components/ui/separator"
import { Button } from "../components/ui/Button"
import { Grid } from '../components/layout/Grid'
import { Toolbar } from "../components/layout/Toolbar";
import { usePapers } from "../hooks/usePapers";
import { type GetPapersResponse } from "../types/api";
import { useNavigate } from "react-router-dom"
import { UploadPaperDialog } from "../components/UploadPaperDialog";

const Papers = () => 
{
  const { papers, uploadPaper } = usePapers();
  const navigate = useNavigate(); 

  const openPaperDetails = (index: number) => {
    const id = papers[index].id
    navigate(`/papers/${id}`)
  }

  return (
    <>
      <Toolbar title="Your Papers">
        <UploadPaperDialog projectId={undefined}>
          <Button className="ml-auto">Upload Paper</Button>
        </UploadPaperDialog>
      </Toolbar>

      <Grid>
        {papers.map((item: GetPapersResponse, cardIndex: number) => 
        {
          return (
            <Card onClick={() => openPaperDetails(cardIndex)} key={`paper-${cardIndex}`}>
              <CardHeader className="text-center">{item.title}</CardHeader>
              <CardDescription className="text-center">{item.authors}</CardDescription>
              <Separator></Separator>
              <div className="flex px-2 flex-wrap gap-2 justify-start">
                <h2>Tags</h2>
                  {item.tags && item.tags.map((tag) => (
                    <Badge>{tag}</Badge>
                  ))}
              </div>
            </Card>
          )
        })}
      </Grid>
    </>
  )
}

export default Papers