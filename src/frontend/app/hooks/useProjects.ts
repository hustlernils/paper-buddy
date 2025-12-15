import { type GetProjectsResponse, type CreateProjectRequest } from "../types/api"
import { useState, useEffect } from "react"
import { useFetch } from "./useFetch"

export interface UseProjectsResponse{
    project: CreateProjectRequest
    projects: GetProjectsResponse[],
    handleProjectChange: (key: string, value: string) => void,
    createProject: () => Promise<void>, 
    refetch: () => Promise<void>
}

export const useProjects = (): UseProjectsResponse => {
  const [project, setProject] = useState<CreateProjectRequest>({title: ""})
  const [projects, setProjects] = useState<GetProjectsResponse[]>([])
  const { api } = useFetch();

  const fetchProjects = async () => {
    const projectResponse = await api.get<GetProjectsResponse[]>('/projects');
    setProjects(projectResponse);
  }

  const createProject = async () => {
    const createProjectResponse = await api.post<string>('/projects', project)
    fetchProjects();
  }

  useEffect(() => {
    fetchProjects();
  }, [])

  const handleProjectChange = (key: string, value: string) =>{
    setProject({
      ...project,
      [key]: value
    })
  }

  return { project, projects, handleProjectChange, createProject, refetch: fetchProjects}
}