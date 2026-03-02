export const TypingAnimation = () =>
{
  return (
    <div className="flex items-center space-x-1">
      <span 
        className="w-2 h-2 bg-secondary-foreground rounded-full animate-bounce inline-block"
        style={{ animationDelay: '0s' }}
      />
      <span 
        className="w-2 h-2 bg-secondary-foreground rounded-full animate-bounce inline-block"
        style={{ animationDelay: '0.2s' }}
      />
      <span 
        className="w-2 h-2 bg-secondary-foreground rounded-full animate-bounce inline-block"
        style={{ animationDelay: '0.4s' }}
      />
    </div>
  )
}