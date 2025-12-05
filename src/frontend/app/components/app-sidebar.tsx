import { Link } from 'react-router-dom'
import {
    Sidebar,
    SidebarContent,
    SidebarGroup,
    SidebarGroupContent,
    SidebarGroupLabel, SidebarHeader,
    SidebarMenu,
    SidebarMenuButton,
    SidebarMenuItem,
    SidebarTrigger,
} from "./ui/sidebar"
import { ModeToggle} from "./mode-toggle";
import { FileText, Folder } from "lucide-react";


const items = [
    {
        title: "Your papers",
        url: "/papers",
        icon: FileText
    },
    {
        title: "Projects",
        url: "/projects",
        icon: Folder
    }
]

export function AppSidebar() {
    return (
        <Sidebar side="left" collapsible="icon">
            <SidebarHeader>
                <div className="flex justify-between items-center">
                    <ModeToggle/>
                    <SidebarTrigger />
                </div>
            </SidebarHeader>
            <SidebarContent>
                
                <SidebarGroup>
                    <SidebarGroupLabel>Papers</SidebarGroupLabel>
                    <SidebarGroupContent>
                        <SidebarMenu>
                            {items.map((item) => (
                                <SidebarMenuItem key={item.title}>
                                    <SidebarMenuButton asChild tooltip={item.title}>
                                        <Link to={item.url}>
                                            <item.icon />
                                            <span>{item.title}</span>
                                        </Link>
                                    </SidebarMenuButton>
                                </SidebarMenuItem>
                            ))}
                        </SidebarMenu>
                    </SidebarGroupContent>
                </SidebarGroup>
            </SidebarContent>
        </Sidebar>
    )
}