import Toolbar from "../components/layout/Toolbar";
import {
  Dialog,
  DialogTrigger,
  DialogContent,
  DialogTitle,
  DialogHeader,
  DialogFooter,
  DialogClose} from "../components/ui/Dialog";
import { Button } from "../components/ui/Button";
import React, { type FormEvent } from "react";
import { Label } from "../components/ui/Label";
import { Input} from "../components/ui/Input";
import { type GetProjectsResponse } from "../types/api";
import Grid from '../components/layout/Grid'
import { Card, CardDescription, CardHeader } from "../components/ui/Card"
import { useProjects } from "../hooks/useProjects";

const Projects = () => 
{

  const { project, projects, createProject, handleProjectChange } = useProjects();

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
            <form onSubmit={(e: FormEvent<HTMLFormElement>) => 
            {
              e.preventDefault();
              createProject();
            }}>
              <div className="grid gap-4">
                <div className="grid w-full max-w-sm items-center gap-3">
                  <Label htmlFor="project">Title</Label>
                  <Input id="project-title" 
                    type="form" 
                    value={project.title} 
                    placeholder="The title of your project" 
                    onChange={
                      (e: React.ChangeEvent<HTMLInputElement>) => handleProjectChange("title", e.target.value)}/>
                  <Label htmlFor="paper">Description</Label>
                  <Input id="project-description" 
                    type="form" 
                    value={project.description} 
                    placeholder="What is your project about?" 
                    onChange={(e: React.ChangeEvent<HTMLInputElement>) => handleProjectChange("description", e.target.value)}/>
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
        {projects.map((item: GetProjectsResponse, cardIndex: number) => 
        {
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