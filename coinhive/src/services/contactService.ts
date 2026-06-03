
const contact_URL = "https://localhost:7143/api/v1/contact";
export const contactService = {
    SendMessage = async (name : string , email : string , message : string , subject : string): Promise<void> => {
        const res = await fetch(`${contact_URL}`, {
            method: "POST",
            headers: { "content-type": "application/json" },
            body: JSON.stringify({ email, name , message, subject }),
        });
        if (!res.ok) throw new Error("Send message failed");
         
    }
}
