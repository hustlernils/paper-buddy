import { useEffect, useState } from "react";
import { type GetPapersResponse } from "../types/api";

export interface UsePapersResponse{
    papers: GetPapersResponse[], 
    isLoading: boolean, 
    error: string | null, 
    uploadPaper: (file: File | null) => Promise<void>, 
    refetch: () => Promise<void>
}

export const usePapers = (): UsePapersResponse => {
    const [papers, setPapers] = useState<GetPapersResponse[]>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [error, setError] = useState<string | null>(null);

    const fetchPapers = async () => {
        try {

            setIsLoading(true);

            const response = await fetch('http://localhost:5009/papers', {
                method: 'GET',
            });

            if (!response.ok) {
                throw new Error("Error while fetching data!");
            }

            const data = await response.json();
            setPapers(data);
        } 
        catch (error) {
            setError((error as Error).message)
        }
        finally{
            setIsLoading(false);
        }
    };

    const uploadPaper = async (file: File | null) => {
        try{
            if (!file)
            {
                setError("No File selected. Choose a file to upload please.")
                return;
            }

            const formData = new FormData();
            formData.append("file", file)

            const response = await fetch('http://localhost:5009/papers/upload', {
                method: 'POST',
                body: formData,
            });

            if (!response.ok) {
                throw new Error("Error while uploading data!");
            }

            const data  = await response.json();
            console.log(data);
        }
        catch (error) {
            setError((error as Error).message)
        }
    }

    useEffect(() =>{
        fetchPapers();
    }, [])

    return { papers, isLoading, error, uploadPaper, refetch: fetchPapers }
}