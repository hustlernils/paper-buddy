export const TypingAnimation = () =>
{
  return (
    <div className="flex items-center space-x-1">
      <span 
        className="w-2 h-2 bg-secondary-foreground rounded-full animate-bounce inline-block"/>
      <span 
        className="w-2 h-2 bg-secondary-foreground rounded-full animate-bounce inline-block delay-150"
      />
      <span 
        className="w-2 h-2 bg-secondary-foreground rounded-full animate-bounce inline-block delay-300"
      />
    </div>
  )
}