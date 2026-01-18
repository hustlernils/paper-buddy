import { useParams } from "react-router-dom";
import { usePapers } from "../hooks/usePapers";
import { Toolbar } from "../components/layout/Toolbar";

export const PaperDetails = () => {
  const { paperId } = useParams<{paperId: string}>(); 
  const { paperDetails } = usePapers(paperId);

  return (
    <>
      <Toolbar title={paperDetails?.title ?? "Unknown Title"}>
      </Toolbar>
    <div>
      <h1>Summary</h1>
      {paperDetails?.summary}
    </div>
    </> 
  )
}