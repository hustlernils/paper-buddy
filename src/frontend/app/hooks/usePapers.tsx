import { useEffect, useMemo, useReducer } from "react";
import { type GetPapersResponse } from "../types/api";
import { papersReducer } from "../reducers/papersReducer";
import { createPapersActions } from '../actions/papersActions'

export interface UsePapersResponse{
    papers: GetPapersResponse[], 
    isLoading: boolean, 
    error: string | null, 
    uploadPaper: (file: File | null) => Promise<void>, 
    refetch: () => Promise<void>
}

export const usePapers = (): UsePapersResponse => {

    const [state, dispatch] = useReducer(papersReducer, { papers: [], isLoading: false, error: null})
    const { papers, isLoading, error } = state;

    const actions = useMemo(() => createPapersActions(dispatch), [dispatch]) 
    
    const fetchPapers = async () => {
        try {
            actions.fetchStart();

            const response = await fetch('http://localhost:5009/papers', {
                method: 'GET',
            });

            if (!response.ok) {
                throw new Error("Error while fetching data!");
            }

            const data = await response.json();
            actions.fetchSuccess(data);
        } 
        catch (error) {
            actions.setError((error as Error).message)}
    };

    const uploadPaper = async (file: File | null) => {
        try{
            if (!file)
            {
                actions.setError("No File selected. Choose a file to upload please.");
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
            actions.setError((error as Error).message)
        }
        
        await fetchPapers();
    }

    useEffect(() =>{
        fetchPapers();
    }, [])

    return { papers, isLoading, error, uploadPaper, refetch: fetchPapers }
}