import { type GetPaperDetailsResponse, type GetPapersResponse } from "../types/api"

export type PapersAction = {type: "FETCH_START"} | {type: "FETCH_SUCCESS", payload: GetPapersResponse[]} | {type: "FETCH_ERROR", payload: string} | {type: "SET_PAPER_DETAILS", payload: GetPaperDetailsResponse | null}

interface PapersState{
    papers: GetPapersResponse[],
    paperDetails: GetPaperDetailsResponse | null
    isLoading: boolean,
    error: string | null
}

export const papersReducer = (state: PapersState, action: PapersAction): PapersState => 
{
  switch (action.type) 
  {
  case "FETCH_START":
    return {
      ...state,
      isLoading: true,
      error: null
    }
  case "FETCH_SUCCESS":
    return {
      ...state,
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
  case "SET_PAPER_DETAILS":
    return {
      ...state,
      isLoading: false,
      paperDetails: action.payload
    }
  default: return state;
  }
}