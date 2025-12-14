import { Input } from "../components/ui/input"
import { Label } from "../components/ui/label"
import { Badge } from "../components/ui/badge"
import { Card, CardDescription, CardHeader } from "../components/ui/card"
import { Separator } from "../components/ui/separator"
import { Button } from "../components/ui/button"
import {
    Dialog,
    DialogTrigger,
    DialogContent,
    DialogTitle,
    DialogHeader,
    DialogFooter,
    DialogClose} from "../components/ui/dialog";
import {type ChangeEvent, type FormEvent, useState} from "react";
import Grid from '../components/layout/Grid'
import Toolbar from "../components/layout/Toolbar";
import { usePapers } from "../hooks/usePapers";
import { type GetPapersResponse } from "../types/api";

const Papers = () => {
    const [file, setFile] = useState<File | null>(null);
    const { papers, isLoading, error, uploadPaper } = usePapers();
   
    return (
        <>
            {isLoading && <div>Loading...</div>}
            {error && <div>Something went wrong</div>}
            <Toolbar title="Your Papers">
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
                            uploadPaper(file).then(() => console.log("success!"));
                        }}>
                        <div className="grid gap-4">
                            <div className="grid w-full max-w-sm items-center gap-3">
                                <Label htmlFor="paper">Choose a file to upload.</Label>
                                    <Input id="paper-upload" accept="application/pdf" type="file" onChange={(e: ChangeEvent<HTMLInputElement>) => setFile((e.target as HTMLInputElement).files?.[0] || null)}/>
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
            </Toolbar>

            <Grid>
                {papers.map((item: GetPapersResponse, cardIndex: number) => {
                    return (
                        <Card key={`paper-${cardIndex}`}>
                            <CardHeader className="text-center">{item.title}</CardHeader>
                            <CardDescription className="text-center">{item.authors}</CardDescription>
                            <Separator></Separator>
                                <div className="flex px-2 flex-wrap gap-2 justify-start">
                                    <h2>Tags</h2>
                                    <Badge>Example tag</Badge>                                
                                    <Badge>tag</Badge>
                                </div>
                        </Card>
                    )
                })}
            </Grid>
        </>
    )
}

export default Papers