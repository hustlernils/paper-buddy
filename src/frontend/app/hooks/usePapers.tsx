import { useEffect, useMemo, useReducer } from "react";
import { type GetPapersResponse } from "../types/api";
import { papersReducer } from "../reducers/papersReducer";
import { createPapersActions } from '../actions/papersActions'
import { useFetch } from "./useFetch";

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
  const { api, isLoading: apiLoading } = useFetch();

  const actions = useMemo(() => createPapersActions(dispatch), [dispatch]) 
    
  const fetchPapers = async () => {
    try {
      actions.fetchStart();

      const papersResponse = await api.get<GetPapersResponse[]>('/papers')
      actions.fetchSuccess(papersResponse);
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

      const data = await api.post('/papers/upload', formData, 'form-data')
    }
    catch (error) {
      actions.setError((error as Error).message)
    }
        
    await fetchPapers();
  }

  useEffect(() =>{
    fetchPapers();
  }, [])

  return { papers, isLoading: isLoading || apiLoading, error, uploadPaper, refetch: fetchPapers }
}