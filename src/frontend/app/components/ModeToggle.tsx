import { Moon, Sun } from "lucide-react"
import {useState, useEffect} from "react";

import { Button } from "./ui/Button"
import { useTheme } from "./ThemeProvider"

export function ModeToggle() 
{
  const { setTheme } = useTheme()
  const [darkMode, setDarkMode] = useState(true);

  useEffect(() => 
  {
    if (darkMode)
    {
      setTheme("light");
    }
    else
    {
      setTheme("dark");
    }
  }, [darkMode, setTheme]);

  const toggleTheme = () => 
  {
    setDarkMode(!darkMode);
  }

  return (
    <Button className="group-data-[collapsible=icon]:hidden" variant="outline" size="icon" onClick={() => toggleTheme()}>
      {darkMode
        ? <Sun className="h-[1.2rem] w-[1.2rem] scale-100 rotate-0 transition-all dark:scale-0 dark:-rotate-90" />
        : <Moon className="absolute h-[1.2rem] w-[1.2rem] scale-0 rotate-90 transition-all dark:scale-100 dark:rotate-0" />
      }
    </Button>
  )
}