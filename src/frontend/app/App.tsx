import './App.css'
import { Input } from "./components/ui/input"
import { Label } from "./components/ui/label"
import { Badge } from "./components/ui/badge"
import { Card } from "./components/ui/card"
import {CardDescription, CardHeader} from "./components/ui/card";
import { Separator } from "./components/ui/separator"
import { Button } from "./components/ui/button"
import { SidebarInset } from './components/ui/sidebar'
import {
    Dialog,
    DialogTrigger,
    DialogContent,
    DialogTitle,
    DialogHeader,
    DialogFooter,
    DialogClose} from "./components/ui/dialog";
import {type ChangeEvent, type FormEvent, useEffect, useState} from "react";
import Grid from './components/layout/Grid'

interface GetPapersResponse
{
    id: string,
    title?: string,
    authors: string
}

function App() {

    const [file, setFile] = useState<File | null>(null);
    const [responseData, setResponseData] = useState<GetPapersResponse[] | undefined>(undefined);

    useEffect(() => {
        const fetchPapers = async () => {
            try {
            const response = await fetch('http://localhost:5009/papers', {
                method: 'GET',
            });

            if (!response.ok) {
                throw new Error("Error while fetching data!");
            }

            const data = await response.json();
            console.log(data);

            setResponseData(data);

            } catch (error) {
                console.error(error);
            }
        };

        fetchPapers();
    }, []);

    const uploadPaper = async () =>{

        console.log(file)
        if (!file)
        {
            return;
        }

        const formData = new FormData();
        formData.append("file", file)

        const response = await fetch('http://localhost:5009/papers/upload', {
            method: 'POST',
            body: formData,
        });

        if (!response.ok) {
            throw new Error("Error while uploading data!");
        }

        const data  = await response.json();
        console.log(data);
    }

  return (
    <>
        <SidebarInset>
            <title>PaperBuddy</title>

            <div className='flex mb-10 justify-center'>
                <h1 className="text-5xl font-bold">Your Papers</h1>
                            <Dialog>
                    <DialogTrigger asChild>
                        <Button className="ml-auto">Upload Paper</Button>
                    </DialogTrigger>
                    <DialogContent className="sm:max-w-[425px]">
                        <DialogHeader>
                            <DialogTitle>Upload Paper</DialogTitle>
                        </DialogHeader>
                        <form onSubmit={(e: FormEvent<HTMLFormElement>) => {
                            e.preventDefault();
                            uploadPaper().then(() => console.log("success!"));
                        }}>
                        <div className="grid gap-4">
                            <div className="grid w-full max-w-sm items-center gap-3">
                                <Label htmlFor="paper">Choose a file to upload.</Label>
                                <Input id="paper-upload" accept="application/pdf" type="file" onChange={(e: ChangeEvent<HTMLInputElement>) => setFile(e.target.files[0] || null)}/>
                            </div>
                        </div>
                        <DialogFooter>
                            <DialogClose asChild>
                                <Button variant="outline">Cancel</Button>
                            </DialogClose>
                            <Button type="submit" disabled={!file} >Upload</Button>
                        </DialogFooter>
                        </form>
                    </DialogContent>
                </Dialog>
            </div>

            <Grid>
                {responseData && responseData.map((item: GetPapersResponse, cardIndex: number) => {
                    return (
                        <Card key={`paper-${cardIndex}`}>
                            <CardHeader>{item.title}</CardHeader>
                            <CardDescription>{item.authors}</CardDescription>
                            <Separator></Separator>
                            {/* <div className="w-full flex flex-wrap gap-4 justify-center">
                                <h2>Tags</h2>
                                {item.tags.map((tag: string, index: number) => {
                                    return (<Badge key={`paper-${cardIndex}-tag-${index}`}>{tag}</Badge>)
                                })}
                            </div> */}
                        </Card>
                    )
                })}
            </Grid>

        </SidebarInset>
    </>
  )
}

export default App
