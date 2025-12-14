import { useState } from "react"

export interface ApiRequestOptions{
    method: 'GET' | 'POST'
    body?: any,
    contentType?: 'json' | 'form-data' | 'none'
    headers?: Record<string, string>
}

export const useFetch = () => {
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const makeRequest = async <T>(path: string, options: ApiRequestOptions): Promise<T> => {
        try {
            setIsLoading(true);

            // hardcoded for now
            const BASE_URL = 'http://localhost:5009';
            const contentType = options.contentType || 'json'; // Default to 'json'
            const config: RequestInit = {
                method: options.method,
                headers: options.headers || {}
            }

            if (options.body){
                if(contentType === 'json'){
                    config.headers =  {
                        ...config.headers,
                        "Content-Type": "application/json"
                    }
                    config.body = JSON.stringify(options.body)
                }
                else if (contentType === 'form-data'){
                    config.body = options.body;
                }
            }

            const response = await fetch(`${BASE_URL}${path}`, config)

            if (!response.ok) {
                throw new Error("Error while fetching data!");
            }
            
            const data = await response.json();        
            return data;
        } 
        catch (error) {
            console.log(error);
            throw error;
        }
        finally{
            setIsLoading(false);
        }
    }

    return { isLoading, makeRequest }
}