import { useParams } from "react-router-dom"

export const ProjectDetails = () => {

    const {id} = useParams<{id: string}>(); 

    return(
        <>
        <div>hey there</div>
        <div>{id}</div>
        </>
    )
}