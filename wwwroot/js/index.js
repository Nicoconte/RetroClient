function increaseProgressBar(idProgressBar, percentage) {
    let progressBar = document.getElementById(idProgressBar);

    if (percentage < 100)
    {
        progressBar.classList.add("bg-primary");
	}

    progressBar.value = this.percentage;
    progressBar.style.width = `${percentage}%`
    progressBar.innerText = `${percentage}%`;

    if (percentage === 100)
    {
        progressBar.classList.add("bg-success");
    }
}