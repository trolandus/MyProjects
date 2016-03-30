#pragma once

#pragma region INCLUDES
#include<d3d11.h>
#include<DirectXMath.h>
using namespace DirectX;
#pragma endregion

//Responsible for encapsulating the geometry for 3D models.
class ModelClass
{
private:
	//Definition of our vertex type that will be used with the vertex buffer in this ModelClass.
	//This typedef must match the layout in the ColorShaderClass
	struct VertexType
	{
		XMFLOAT3 position;
		XMFLOAT4 color;
	};

public:
	ModelClass();
	ModelClass(const ModelClass&);
	~ModelClass();

	//The functions here handle initializing and shutdown of the model's vertex and index buffers.
	bool Initialize(ID3D11Device*);
	void Shutdown();
	//Puts the model geometry on the video card to prepare it for drawing by the color shader.
	void Render(ID3D11DeviceContext*);

	int GetIndexCount();

private:
	bool InitializeBuffers(ID3D11Device*);
	void ShutdownBuffers();
	void RenderBuffers(ID3D11DeviceContext*);

	ID3D11Buffer *m_vertexBuffer, *m_indexBuffer;
	int m_vertexCount, m_indexCount;
};

