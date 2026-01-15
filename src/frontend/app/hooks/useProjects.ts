import { type GetProjectsResponse, type CreateProjectRequest } from "../types/api"
import { useState, useEffect } from "react"
import { useFetch } from "./useFetch"

export interface UseProjectsResponse{
    project: CreateProjectRequest
    projects: GetProjectsResponse[],
    getCurrentProject: (projectId: string | undefined ) => GetProjectsResponse | undefined
    handleProjectChange: (key: string, value: string) => void,
    createProject: () => Promise<void>, 
    refetch: () => Promise<void>
}

export const useProjects = (): UseProjectsResponse => 
{
  const [project, setProject] = useState<CreateProjectRequest>({title: ""})
  const [projects, setProjects] = useState<GetProjectsResponse[]>([])
  const { api } = useFetch();

  useEffect(() => 
  {
    fetchProjects();
  }, [])

  const fetchProjects = async () => 
  {
    const projectResponse = await api.get<GetProjectsResponse[]>('/projects');
    if (projectResponse)
    {
      setProjects(projectResponse);
    }  
  }

  const createProject = async () => 
  {
    await api.post<string>('/projects', project)
    fetchProjects();
  }

  const handleProjectChange = (key: string, value: string) =>
  {
    setProject({
      ...project,
      [key]: value
    })
  }

  const getCurrentProject = (projectId: string | undefined) => projects.find(p => p.id === projectId)

  return { project, projects, getCurrentProject, handleProjectChange, createProject, refetch: fetchProjects}
}