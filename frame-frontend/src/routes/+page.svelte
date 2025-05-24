<script>
  import { onMount } from "svelte";
    let images = $state([]);
    let files = $state();
    let settingImage = $state(false);
    let basePath = 'http://localhost:3030/'
    const getImages = async ()=>{
        let imageRequest = (await fetch(basePath+"picture/list"));
        let json = await imageRequest.json();
        console.log(json);
        images = json;
    }
    const setImage = async (image)=>{
        settingImage = true;
        console.log(`Setting image to ${image.filePath}`);
        //let setRequest = await fetch("")
        settingImage=false;
    }
    const upload = async ()=>{
        let formData = new FormData();
        formData.append("files",files[0]); 
        console.log(files);
        const options = {method: 'POST'};

        options.body = formData;
        let url = basePath+"picture";
        try {
        const response = await fetch(url, options);
        console.log(response);
        } catch (error) {
        console.error(error);
        }
        await getImages();
    }
    onMount(()=>{
        getImages();
    })
</script>
<h1>Frame!</h1>
<div class="images">
{#each images as image}
<button on:click={()=>setImage(image)}>
    <img src={`${basePath}${image.hostedPath}`} class="thumbnail" alt="Thumbnail"/>
</button>
    
{/each}
</div>
<div class="">
    <form>
<input type="file" name="files" bind:files>
<button on:click={upload}>Upload</button>
    </form>

</div>

<style>
    .images{
        display: flex;
        gap:3rem;
    }
    .thumbnail{
        width:100px;
    }
</style>