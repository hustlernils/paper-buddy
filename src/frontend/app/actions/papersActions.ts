import type { GetPapersResponse } from "../types/api"
import { type Dispatch } from "react"
import { type PapersAction } from "../reducers/papersReducer"

interface PapersActions{
    fetchStart: () => void,
    fetchSuccess: (papers: GetPapersResponse[]) => void, 
    setError: (error: string ) => void 
}

export const createPapersActions = (
  dispatch: Dispatch<PapersAction>
): PapersActions => ({
  fetchStart: () =>
    dispatch({ type: "FETCH_START" }),

  fetchSuccess: (papers) =>
    dispatch({ type: "FETCH_SUCCESS", payload: papers }),

  setError: (error) =>
    dispatch({ type: "FETCH_ERROR", payload: error }),
});

