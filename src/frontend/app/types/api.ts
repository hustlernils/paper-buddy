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

export type ParentType = "project" | "paper"

export interface ChatResponse {
    id: string,
    createdAt: string
};

export interface ChatMessageResponse {
    role: string, 
    content: string,
    createdAt: string
}

export interface CreateChatRequest {
    parentType: string,
    parentId: string
}