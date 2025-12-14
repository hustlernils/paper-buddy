import { type GetProjectsResponse, type CreateProjectRequest } from "../types/api"

export interface UseProjectsResponse{
    projects: GetProjectsResponse[],
    isLoading: boolean, 
    error: string | null, 
    createProject: (request: CreateProjectRequest) => Promise<void>, 
    refetch: () => Promise<void>
}

export const useProjects = (): UseProjectsResponse => {

    return { projects, isLoading, error, createProject, refetch}
}