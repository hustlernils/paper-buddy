export interface GetPapersResponse
{
    id: string,
    title?: string,
    authors: string
}

export interface GetProjectsResponse{
    id: string,
    title: string,
    description?: string
}

export interface CreateProjectRequest{
    title: string,
    description?: string
}