const urlInput = document.getElementById('urlInput');
const shortenButton = document.getElementById('shortenButton');
const shortenedUrl = document.getElementById('shortenedUrl');
const shortUrl = document.getElementById('shortUrl');
const copyButton = document.getElementById('copyButton');
const hostname = "127.0.0.1";

shortenButton.addEventListener('click', async () => {
    const longUrl = urlInput.value;
    if (!longUrl) {
        alert('Please enter a URL to shorten');
        return;
    }

    try {
        const response = await fetch(`http://${hostname}:5149/shorten?url=${longUrl}`);
        let data = await response.text(); // Read the response as text
        data = data.replace("localhost", hostname);
        console.log(data);

        if (response.ok) {
            shortUrl.textContent = data;
            shortenedUrl.classList.remove('hidden');
        } else {
            alert(`Error: ${data}`);
        }
    } catch (error) {
        console.error(error);
    }

});

copyButton.addEventListener('click', () => {
    const shortenedText = shortUrl.textContent;
    if (!shortenedText) {
        return;
    }

    navigator.clipboard.writeText(shortenedText)
        .then(() => {
            copyButton.innerText = "Copied!";
        })
        .catch(err => {
            console.error('Failed to copy link:', err);
            alert('Failed to copy link to clipboard');
        });
});