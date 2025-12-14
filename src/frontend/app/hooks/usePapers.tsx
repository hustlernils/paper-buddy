import { useEffect, useReducer } from "react";
import { type GetPapersResponse } from "../types/api";

export interface UsePapersResponse{
    papers: GetPapersResponse[], 
    isLoading: boolean, 
    error: string | null, 
    uploadPaper: (file: File | null) => Promise<void>, 
    refetch: () => Promise<void>
}

type PapersAction = {type: "FETCH_START"} | {type: "FETCH_SUCCESS", payload: GetPapersResponse[]} | {type: "FETCH_ERROR", payload: string} 

interface PapersState{
    papers: GetPapersResponse[],
    isLoading: boolean,
    error: string | null
}

const papersReducer = (state: PapersState, action: PapersAction): PapersState => {
    switch (action.type) {
        case "FETCH_START":
            return {
                ...state,
                isLoading: true,
                error: null
            }
        case "FETCH_SUCCESS":
            return {
                papers: action.payload,
                isLoading: false,
                error: null
            }
        case "FETCH_ERROR":
            return {
                ...state,
                isLoading: false,
                error: action.payload
            }
        default: return state;
    }
}

export const usePapers = (): UsePapersResponse => {

    const [state, dispatch] = useReducer(papersReducer, { papers: [], isLoading: false, error: null})
    const { papers, isLoading, error } = state;
    // const [papers, setPapers] = useState<GetPapersResponse[]>([]);
    // const [isLoading, setIsLoading] = useState<boolean>(false);
    // const [error, setError] = useState<string | null>(null);

    const fetchPapers = async () => {
        try {
            dispatch({type: "FETCH_START"})

            const response = await fetch('http://localhost:5009/papers', {
                method: 'GET',
            });

            if (!response.ok) {
                throw new Error("Error while fetching data!");
            }

            const data = await response.json();
            dispatch({type: "FETCH_SUCCESS", payload: data})
        } 
        catch (error) {
            dispatch({type: "FETCH_ERROR", payload: (error as Error).message})
        }
    };

    const uploadPaper = async (file: File | null) => {
        try{
            if (!file)
            {
                dispatch({type: "FETCH_ERROR", payload: "No File selected. Choose a file to upload please."})
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
            dispatch({type: "FETCH_ERROR", payload: (error as Error).message})
        }

        await fetchPapers();
    }

    useEffect(() =>{
        fetchPapers();
    }, [])

    return { papers, isLoading, error, uploadPaper, refetch: fetchPapers }
}