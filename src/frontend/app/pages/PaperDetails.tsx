import { useParams } from "react-router-dom";
import { usePapers } from "../hooks/usePapers";

export const PaperDetails = () => {
  const { paperId } = useParams<{paperId: string}>(); 
  const { papers } = usePapers();

  return (
    <div>
      
    </div>
  )

}