import { type GetPapersResponse } from "../types/api"

export type PapersAction = {type: "FETCH_START"} | {type: "FETCH_SUCCESS", payload: GetPapersResponse[]} | {type: "FETCH_ERROR", payload: string} 

interface PapersState{
    papers: GetPapersResponse[],
    isLoading: boolean,
    error: string | null
}

export const papersReducer = (state: PapersState, action: PapersAction): PapersState => {
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