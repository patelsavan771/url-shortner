const urlInput = document.getElementById('urlInput');
const shortenButton = document.getElementById('shortenButton');
const shortenedUrl = document.getElementById('shortenedUrl');
const shortUrl = document.getElementById('shortUrl');
const copyButton = document.getElementById('copyButton');

shortenButton.addEventListener('click', async () => {
    const longUrl = urlInput.value;
    if (!longUrl) {
        alert('Please enter a URL to shorten');
        return;
    }

    try {
        const response = await fetch(`http://localhost:5149/shorten?url=${longUrl}`);
        const data = await response.text(); // Read the response as text
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
        return; // No shortened URL to copy
    }

    navigator.clipboard.writeText(shortenedText)
        .then(() => {
            alert('Link copied to clipboard!');
        })
        .catch(err => {
            console.error('Failed to copy link:', err);
            alert('Failed to copy link to clipboard');
        });
});