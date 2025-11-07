import './App.css'
import { Input } from "./components/ui/input"
import { Label } from "./components/ui/label"
import { Badge } from "./components/ui/badge"
import { Card } from "./components/ui/card"
import {CardDescription, CardHeader} from "./components/ui/card";
import { Separator } from "./components/ui/separator"
import { Button } from "./components/ui/button"
import { ModeToggle} from "./components/mode-toggle";
import {
    Dialog,
    DialogTrigger,
    DialogContent,
    DialogTitle,
    DialogHeader,
    DialogFooter,
    DialogClose} from "./components/ui/dialog";
import {type ChangeEvent, type FormEvent, useEffect, useState} from "react";

interface Paper {
    title: string,
    description: string,
    tags: string[]
}

function App() {

    const [file, setFile] = useState<File | null>(null);

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
        } catch (error) {
        console.error(error);
        }
    };

    fetchPapers();
    }, []);


    const data: Paper[] = [
        {
            title: "Deep Learning for Medical Image Segmentation",
            description:
                "An overview of convolutional neural network architectures applied to organ and tumor segmentation tasks in MRI and CT scans.",
            tags: ["AI", "Medical Imaging", "Segmentation", "Deep Learning"],
        },
        {
            title: "Quantum Entanglement in Large-Scale Systems",
            description:
                "Experimental results demonstrating quantum entanglement in macroscopic mechanical systems under cryogenic conditions.",
            tags: ["Quantum Physics", "Entanglement", "Experimental"],
        },
        {
            title: "Transformer-Based Protein Sequence Modeling",
            description:
                "Introducing a transformer architecture for predicting protein folding and secondary structure directly from amino acid sequences.",
            tags: ["AI", "Bioinformatics", "Transformers"],
        },
        {
            title: "Climate Change Impacts on Arctic Sea Ice Dynamics",
            description:
                "A 30-year satellite analysis of seasonal variability and thickness decline in Arctic sea ice due to climate forcing.",
            tags: ["Climate Science", "Remote Sensing", "Arctic"],
        },
        {
            title: "Graph Neural Networks for Molecular Property Prediction",
            description:
                "A graph convolutional model trained on chemical datasets to predict solubility, toxicity, and molecular reactivity.",
            tags: ["AI", "Chemistry", "Graph Neural Networks"],
        },
        {
            title: "Advances in CRISPR Gene Editing Precision",
            description:
                "A comprehensive study of off-target detection methods and enhanced Cas9 variants for improved editing accuracy.",
            tags: ["Genetics", "CRISPR", "Biotechnology"],
        },
        {
            title: "Neural Radiance Fields for 3D Scene Reconstruction",
            description:
                "Exploring the use of neural radiance fields (NeRFs) for generating photorealistic 3D reconstructions from sparse 2D images.",
            tags: ["Computer Vision", "3D Reconstruction", "NeRF"],
        },
        {
            title: "Large Language Models as Scientific Assistants",
            description:
                "Evaluating GPT-style models for literature summarization, hypothesis generation, and data interpretation in research workflows.",
            tags: ["AI", "Natural Language Processing", "Research Tools"],
        },
        {
            title: "Battery Performance Optimization via Reinforcement Learning",
            description:
                "A reinforcement learning framework for discovering optimal charge–discharge strategies in lithium-ion batteries.",
            tags: ["Energy", "Reinforcement Learning", "Optimization"],
        },
        {
            title: "Robust Statistical Methods for Genomic Data Analysis",
            description:
                "Proposing a new Bayesian hierarchical model to handle high-dimensional genomic data with missing values and noise.",
            tags: ["Statistics", "Genomics", "Bayesian Modeling"],
        },
    ];

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
        <title>PaperBuddy</title>

        <h1 className="text-5xl font-bold">Your Papers</h1>

        <div className="w-full flex flex-col gap-6">

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

            <ModeToggle></ModeToggle>

            <div className="w-full flex flex-wrap gap-6 justify-center">
                {data.map((item: Paper, cardIndex: number) => {

                    return (
                        <Card className="p-6 max-w-3xs" key={`paper-${cardIndex}`}>
                            <CardHeader>{item.title}</CardHeader>
                            <CardDescription>{item.description}</CardDescription>
                            <Separator></Separator>
                            <div className="w-full flex flex-wrap gap-4 justify-center">
                                <h2>Tags</h2>
                                {item.tags.map((tag: string, index: number) => {
                                    return (<Badge key={`paper-${cardIndex}-tag-${index}`}>{tag}</Badge>)
                                })}
                            </div>
                        </Card>
                    )
                })}
            </div>
        </div>
    </>
  )
}

export default App
