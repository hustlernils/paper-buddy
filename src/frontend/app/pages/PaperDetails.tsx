import { useParams } from "react-router-dom";
import { usePapers } from "../hooks/usePapers";
import { Toolbar } from "../components/layout/Toolbar";
import { Card, CardContent, CardTitle } from "../components/ui/Card";
import { Separator } from "../components/ui/separator";

export const PaperDetails = () => 
{
  const { paperId } = useParams<{paperId: string}>(); 
  const { paperDetails } = usePapers(paperId);

  return (
    <>
      <Toolbar title={paperDetails?.title ?? "Unknown Title"}>
      </Toolbar>
      <Card>
        <CardTitle className="ml-6">Paper Summary</CardTitle>
        <Separator></Separator>
        <CardContent>
          {paperDetails?.summary}
        </CardContent>
      </Card>
    </> 
  )
}