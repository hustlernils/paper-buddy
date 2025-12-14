import Toolbar from "../components/layout/Toolbar";
import {
    Dialog,
    DialogTrigger,
    DialogContent,
    DialogTitle,
    DialogHeader,
    DialogFooter,
    DialogClose} from "../components/ui/dialog";
import { Button } from "../components/ui/button";
import React, { type FormEvent, useEffect, useState } from "react";
import { Label } from "../components/ui/label";
import { Input} from "../components/ui/input";
import { type GetProjectsResponse, type CreateProjectRequest } from "../types/api";
import Grid from '../components/layout/Grid'
import { Card, CardDescription, CardHeader } from "../components/ui/card"
import { useFetch } from "../hooks/useFetch";

const Projects = () => {
    const [project, setProject] = useState<CreateProjectRequest>({title: ""})
    const [projects, setProjects] = useState<GetProjectsResponse[]>([])
    const { makeRequest } = useFetch();

    const fetchProjects = async () => {
        const projectResponse = await makeRequest<GetProjectsResponse[]>('/projects', { method: 'GET' });
        setProjects(projectResponse);
    }

    const createProject = async () => {
        const createProjectResponse = await makeRequest<string>('/projects', {method: 'POST', body: project})
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

    return (
        <>
            <Toolbar title="Projects">
                <Dialog>
                    <DialogTrigger asChild>
                        <Button className="ml-auto">New Project</Button>
                    </DialogTrigger>
                    <DialogContent className="sm:max-w-[425px]">
                        <DialogHeader>
                            <DialogTitle>New Project</DialogTitle>
                        </DialogHeader>
                        <form onSubmit={(e: FormEvent<HTMLFormElement>) => {
                            e.preventDefault();
                            createProject();
                        }}>
                        <div className="grid gap-4">
                            <div className="grid w-full max-w-sm items-center gap-3">
                                <Label htmlFor="project">Title</Label>
                                    <Input id="project-title" type="form" value={project.title} placeholder="The title of your project" onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleProjectChange("title", e.target.value)}/>
                                <Label htmlFor="paper">Description</Label>
                                    <Input id="project-description" type="form" value={project.description} placeholder="What is your project about?" onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleProjectChange("description", e.target.value)}/>
                            </div>
                        </div>
                        <DialogFooter>
                            <DialogClose asChild>
                                <Button variant="outline">Cancel</Button>
                            </DialogClose>
                            <Button type="submit" disabled={false} >Create</Button>
                        </DialogFooter>
                        </form>
                    </DialogContent>
                </Dialog>
            </Toolbar>

            <Grid>
                {projects.map((item: GetProjectsResponse, cardIndex: number) => {
                    return (
                        <Card key={`paper-${cardIndex}`}>
                            <CardHeader className="text-center">{item.title}</CardHeader>
                            <CardDescription className="text-center">{item.description}</CardDescription>
                        </Card>
                    )
                })}
            </Grid>
        </>
    );
}

export default Projects;