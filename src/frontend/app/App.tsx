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

function App() {

    interface Paper {
      title: string,
        description: string,
        tags: string[]
    }

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

  return (
    <>
        <title>PaperBuddy</title>

        <h1 className="text-5xl font-bold">Your Papers</h1>

        <div className="w-full flex flex-col gap-6">

            <Dialog>
                <form>
                    <DialogTrigger asChild>
                        <Button className="ml-auto">Upload Paper</Button>
                    </DialogTrigger>
                    <DialogContent className="sm:max-w-[425px]">
                        <DialogHeader>
                            <DialogTitle>Upload Paper</DialogTitle>
                        </DialogHeader>
                        <div className="grid gap-4">
                            <div className="grid w-full max-w-sm items-center gap-3">
                                <Label htmlFor="paper">Choose a file to upload.</Label>
                                <Input id="paper-upload" type="file" />
                            </div>
                        </div>
                        <DialogFooter>
                            <DialogClose asChild>
                                <Button variant="outline">Cancel</Button>
                            </DialogClose>
                            <Button type="submit">Upload</Button>
                        </DialogFooter>
                    </DialogContent>
                </form>
            </Dialog>

            <ModeToggle></ModeToggle>

            <div className="w-full flex flex-wrap gap-6 justify-center">
                {data.map((item: Paper) => {

                    return (
                        <Card className="p-6 max-w-3xs">
                            <CardHeader>{item.title}</CardHeader>
                            <CardDescription>{item.description}</CardDescription>
                            <Separator></Separator>
                            <div className="w-full flex flex-wrap gap-4 justify-center">
                                <h2>Tags</h2>
                                {item.tags.map((tag: string) => {
                                    return (<Badge>{tag}</Badge>)
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
