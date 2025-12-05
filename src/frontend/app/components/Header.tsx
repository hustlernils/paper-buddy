export interface HeaderProps{
    label: string
}

const Header = ({ label } : HeaderProps) =>{
    return (
        <h1 className="text-xl font-bold">{label}</h1>
    )
}

export default Header;