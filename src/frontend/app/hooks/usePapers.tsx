import { useEffect, useMemo, useReducer } from "react";
import { type GetPapersResponse } from "../types/api";
import { papersReducer } from "../reducers/papersReducer";
import { createPapersActions } from '../actions/papersActions'
import { useFetch } from "./useFetch";

export interface UsePapersResponse{
    papers: GetPapersResponse[], 
    uploadPaper: (file: File | null) => Promise<void>, 
    refetch: () => Promise<void>
}

export const usePapers = (): UsePapersResponse => 
{
  const [state, dispatch] = useReducer(papersReducer, { papers: [], isLoading: false, error: null})
  const { papers } = state;
  const { api } = useFetch();

  const actions = useMemo(() => createPapersActions(dispatch), [dispatch]) 
    
  const fetchPapers = async () => 
  {
    try 
    {
      actions.fetchStart();

      const papersResponse = await api.get<GetPapersResponse[]>('/papers')
      if (papersResponse)
      {
        actions.fetchSuccess(papersResponse);
      }
      
    } 
    catch (error) 
    {
      actions.setError((error as Error).message)
    }
  };

  const uploadPaper = async (file: File | null) => 
  {
    try
    {
      if (!file)
      {
        actions.setError("No File selected. Choose a file to upload please.");
        return;
      }

      const formData = new FormData();
      formData.append("file", file)

      await api.post('/papers/upload', formData, 'form-data')
    }
    catch (error) 
    {
      actions.setError((error as Error).message)
    }
        
    await fetchPapers();
  }

  useEffect(() =>
  {
    fetchPapers();
  }, [])

  return { papers, uploadPaper, refetch: fetchPapers }
}