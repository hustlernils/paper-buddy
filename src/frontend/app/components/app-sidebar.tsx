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
import { FileText, MessageCircle } from "lucide-react";


const items = [
    {
        title: "Your papers",
        url: "#",
        icon: FileText
    },
    {
        title: "Chat",
        url: "#",
        icon: MessageCircle
    }
]

export function AppSidebar() {
    return (
        <Sidebar side="left" collapsible="icon">
            <SidebarHeader>
                <div className="flex justify-between">
                    <ModeToggle/>
                    <SidebarTrigger/>
                </div>
            </SidebarHeader>
            <SidebarContent>
                
                <SidebarGroup>
                    <SidebarGroupLabel>Papers</SidebarGroupLabel>
                    <SidebarGroupContent>
                        <SidebarMenu>
                            {items.map((item) => (
                                <SidebarMenuItem key={item.title}>
                                    <SidebarMenuButton asChild>
                                        <a href={item.url}>
                                            <item.icon />
                                            <span>{item.title}</span>
                                        </a>
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