import React, { type ChangeEvent, type FormEvent, useState } from "react";
import { Button } from "./ui/Button";
import { Input } from "./ui/Input";
import { Label } from "./ui/Label";
import {
  Dialog,
  DialogClose,
  DialogContent,
  DialogFooter,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "./ui/Dialog";
import { usePapers } from "../hooks/usePapers";

export interface UploadPaperDialogProps 
{
  children: React.ReactNode;
}

export const UploadPaperDialog = ({ children }: UploadPaperDialogProps) => 
{
  const [file, setFile] = useState<File | null>(null);
  const { uploadPaper } = usePapers();

  const handleSubmit = async (e: FormEvent<HTMLFormElement>) => 
  {
    e.preventDefault();
    await uploadPaper(file);
    console.log("success!");
  };

  const handleFileChange = (e: ChangeEvent<HTMLInputElement>) => 
  {
    setFile((e.target as HTMLInputElement).files?.[0] || null);
  };

  return (
    <Dialog>
      <DialogTrigger asChild>
        {children}
      </DialogTrigger>
      <DialogContent className="sm:max-w-[425px]">
        <DialogHeader>
          <DialogTitle>Upload Paper</DialogTitle>
        </DialogHeader>
        <form onSubmit={handleSubmit}>
          <div className="grid gap-4">
            <div className="grid w-full max-w-sm items-center gap-3">
              <Label htmlFor="paper">Choose a file to upload.</Label>
              <Input 
                id="paper-upload" 
                accept="application/pdf" 
                type="file" 
                onChange={handleFileChange}
              />
            </div>
          </div>
          <DialogFooter>
            <DialogClose asChild>
              <Button variant="outline">Cancel</Button>
            </DialogClose>
            <Button type="submit" disabled={!file}>Upload</Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
};

export default UploadPaperDialog;